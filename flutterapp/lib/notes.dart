import 'package:flutter/material.dart';

class NotesWidget extends StatelessWidget {
  const NotesWidget({super.key});

  static const TextField myTextField = TextField(
    minLines: 10,
    maxLines: 10,
    decoration: InputDecoration(
      border: OutlineInputBorder(),
      hintText: 'Enter a search term',
    ),
  );

  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return myTextField;
  }

}