import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:flutter_quill/flutter_quill.dart';
import 'editor.dart';
import 'calculator.dart';

void main() {
  runApp(const TabFlutter());
}

class TabFlutter extends StatelessWidget {
  const TabFlutter({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      localizationsDelegates: [
        GlobalMaterialLocalizations.delegate,
        GlobalCupertinoLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
        FlutterQuillLocalizations.delegate,
      ],
      home: DefaultTabController(
        length: 2,
        child: Scaffold(
          appBar: AppBar(
            bottom: TabBar(
              tabs: [
                Tab(text: 'Editor'),
                Tab(text: 'Calculator'),
              ],
            ),
          ),
          body: TabBarView(
            children: [
              Editor(),
              Calculator(),
            ],
          ),
        ),
      ),
    );
  }
}
