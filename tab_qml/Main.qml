import QtQuick
import QtQuick.Controls
import QtQuick.Layouts

import "content"

Window {
    width: 640
    height: 480
    visible: true
    title: qsTr("Hello World")

    ColumnLayout {
        anchors.fill: parent
        TabBar {
            id: bar
            TabButton {
                text: qsTr("Calculator")
            }
            TabButton {
                text: qsTr("Texteditor")

            }

        }

        StackLayout {
            currentIndex: bar.currentIndex
            anchors.fill: parent
            anchors.topMargin: bar.height
            Calculator{}
            Editor{}
        }
    }


}
