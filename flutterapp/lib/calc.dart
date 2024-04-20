import 'package:flutter/material.dart';

class CalcWidget extends StatelessWidget {
  const CalcWidget({super.key});

  static const TextField myTextField = TextField(
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