import 'package:flutter/material.dart';
import 'package:flutterapp/calc.dart';
import 'package:flutterapp/notes.dart';

class Tabs extends StatelessWidget {

  const Tabs({super.key});

  static const List<Tab> myTabs = <Tab> [
    Tab(icon: Icon(Icons.notes), text: 'Notes'),
    Tab(icon: Icon(Icons.calculate), text: 'Calc'),

  ];


  @override
  Widget build(BuildContext context) {
    return DefaultTabController(length: myTabs.length,
        child: Scaffold(
          appBar: AppBar(
            bottom: const TabBar(
              tabs: myTabs,
            ),
          ),
          body: const TabBarView(
            children: [
              NotesWidget(),
              CalcWidget(),
            ],
          ),
        ),
    );
  }

}