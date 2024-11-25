import 'dart:convert';
import 'dart:io' as io show Directory, File;
import 'dart:io';

import 'package:file_picker/file_picker.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter_quill/flutter_quill.dart';
import 'package:flutter_quill_delta_from_html/parser/html_to_delta.dart';
import 'package:tabflutter/quill_delta_sample.dart';
import 'package:flutter_quill_extensions/flutter_quill_extensions.dart';
import 'package:path/path.dart' as path;
import 'package:vsc_quill_delta_to_html/vsc_quill_delta_to_html.dart';

final QuillController _controller = () {
  return QuillController.basic(
      config: QuillControllerConfig(
        clipboardConfig: QuillClipboardConfig(
          enableExternalRichPaste: true,
          onImagePaste: (imageBytes) async {
            if (kIsWeb) {
              // Dart IO is unsupported on the web.
              return null;
            }
            // Save the image somewhere and return the image URL that will be
            // stored in the Quill Delta JSON (the document).
            final newFileName =
                'image-file-${DateTime.now().toIso8601String()}.png';
            final newPath = path.join(
              io.Directory.systemTemp.path,
              newFileName,
            );
            final file = await io.File(
              newPath,
            ).writeAsBytes(imageBytes, flush: true);
            return file.path;
          },
        ),
      ));
}();
final FocusNode _editorFocusNode = FocusNode();
final ScrollController _editorScrollController = ScrollController();

class Editor extends StatelessWidget {
  const Editor({super.key});

  @override
  Widget build(BuildContext context) {
    _controller.document = Document.fromJson(kQuillDefaultSample);
    return Scaffold(
      appBar: AppBar(
        title: const Text('Flutter Quill Example'),
        actions: [

          IconButton(
            icon: const Icon(Icons.output),
            tooltip: 'Print Delta JSON to log',
            onPressed: () {
              ScaffoldMessenger.of(context).showSnackBar(const SnackBar(
                  content:
                      Text('The JSON Delta has been printed to the console.')));
              debugPrint(jsonEncode(_controller.document.toDelta().toJson()));
            },
          ),
        ],
      ),
      body: SafeArea(
        child: Column(
          children: [
            QuillSimpleToolbar(
              controller: _controller,
              config: QuillSimpleToolbarConfig(
                embedButtons: FlutterQuillEmbeds.toolbarButtons(),
                showClipboardCopy: false,
                showClipboardCut: false,
                showClipboardPaste: true,
                customButtons: [
                  QuillToolbarCustomButtonOptions(
                    icon: const Icon(Icons.add_alarm_rounded),
                    onPressed: () {
                      _controller.document.insert(
                        _controller.selection.extentOffset,
                        TimeStampEmbed(
                          DateTime.now().toString(),
                        ),
                      );

                      _controller.updateSelection(
                        TextSelection.collapsed(
                          offset: _controller.selection.extentOffset + 1,
                        ),
                        ChangeSource.local,
                      );
                    },
                  ),
                  QuillToolbarCustomButtonOptions(
                    icon: const Icon(Icons.save_rounded),
                    onPressed: () async {
                      final converter = QuillDeltaToHtmlConverter(
                        List.castFrom(_controller.document.toDelta().toJson()),
                        ConverterOptions.forEmail(),
                      );

                      final html = converter.convert();

                      String? outputFile = await FilePicker.platform.saveFile(
                          dialogTitle: 'Save Your File to desired location',
                          fileName: 'document.html');


                      try {
                        io.File returnedFile = io.File('$outputFile');
                        await returnedFile.writeAsString(html);
                      } catch (e) {}

                    }
                  ),
                  QuillToolbarCustomButtonOptions(
                      icon: const Icon(Icons.add_box_rounded),
                      onPressed: () async {

                        // Lets the user pick one file, but only files with the extensions `html` can be selected
                        FilePickerResult? result = await FilePicker.platform.pickFiles(type: FileType.custom, allowedExtensions: ['html']);

                        // The result will be null, if the user aborted the dialog
                        if(result != null) {
                          File file = File(result.files.first.path!);
                          try {
                            final content = await file.readAsString();
                            final converter = HtmlToDelta().convert(content);
                            _controller.document = Document.fromDelta(converter);
                          } catch (e) {}
                        }



                      }
                  ),
                ],
                buttonOptions: QuillSimpleToolbarButtonOptions(
                  base: QuillToolbarBaseButtonOptions(
                    afterButtonPressed: () {
                      final isDesktop = {
                        TargetPlatform.linux,
                        TargetPlatform.windows,
                        TargetPlatform.macOS
                      }.contains(defaultTargetPlatform);
                      if (isDesktop) {
                        _editorFocusNode.requestFocus();
                      }
                    },
                  ),
                ),
              ),
            ),
            Expanded(
              child: QuillEditor(
                focusNode: _editorFocusNode,
                scrollController: _editorScrollController,
                controller: _controller,
                config: QuillEditorConfig(
                  placeholder: 'Start writing your notes...',
                  padding: const EdgeInsets.all(16),
                  embedBuilders: [
                    ...FlutterQuillEmbeds.editorBuilders(
                      imageEmbedConfig: QuillEditorImageEmbedConfig(
                        imageProviderBuilder: (context, imageUrl) {
                          // https://pub.dev/packages/flutter_quill_extensions#-image-assets
                          if (imageUrl.startsWith('assets/')) {
                            return AssetImage(imageUrl);
                          }
                          return null;
                        },
                      ),
                      videoEmbedConfig: QuillEditorVideoEmbedConfig(
                        customVideoBuilder: (videoUrl, readOnly) {
                          // To load YouTube videos https://github.com/singerdmx/flutter-quill/releases/tag/v10.8.0
                          return null;
                        },
                      ),
                    ),
                    TimeStampEmbedBuilder(),
                  ],
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}

class TimeStampEmbed extends Embeddable {
  const TimeStampEmbed(
    String value,
  ) : super(timeStampType, value);

  static const String timeStampType = 'timeStamp';

  static TimeStampEmbed fromDocument(Document document) =>
      TimeStampEmbed(jsonEncode(document.toDelta().toJson()));

  Document get document => Document.fromJson(jsonDecode(data));
}

class TimeStampEmbedBuilder extends EmbedBuilder {
  @override
  String get key => 'timeStamp';

  @override
  String toPlainText(Embed node) {
    return node.value.data;
  }

  @override
  Widget build(
    BuildContext context,
    EmbedContext embedContext,
  ) {
    return Row(
      children: [
        const Icon(Icons.access_time_rounded),
        Text(embedContext.node.value.data as String),
      ],
    );
  }
}
