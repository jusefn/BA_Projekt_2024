# Copyright (C) 2022 The Qt Company Ltd.
# SPDX-License-Identifier: LicenseRef-Qt-Commercial OR BSD-3-Clause

cmake_minimum_required(VERSION 3.16)
project(apptab_qml LANGUAGES CXX)

find_package(Qt6 REQUIRED COMPONENTS Core Gui Quick QuickControls2)

qt_standard_project_setup(REQUIRES 6.8)

qt_add_executable(apptab_qml WIN32 MACOSX_BUNDLE
    main.cpp
)

qt_add_qml_module(apptab_qml
    URI tab_qml
    NO_RESOURCE_TARGET_PATH
    QML_FILES
        "Main.qml"
        "content/Calculator.qml"
        "content/Editor.qml"
        "content/Display.qml"
        "content/NumberPad.qml"
        "content/CalculatorButton.qml"
        "content/BackspaceButton.qml"
        "content/calculator.js"

    RESOURCES
        "content/images/backspace.svg"
        "content/images/backspace_fill.svg"
        "texteditor.html"
        "fonts/fontello.ttf"
        "images/qt-logo.png"
)

target_link_libraries(apptab_qml PUBLIC
    Qt6::Core
    Qt6::Gui
    Qt6::Quick
    Qt6::QuickControls2
)

if(CMAKE_CROSSCOMPILING)
    target_compile_definitions(apptab_qml PUBLIC
        QT_EXTRA_FILE_SELECTOR="touch"
    )
endif()

install(TARGETS apptab_qml
    BUNDLE  DESTINATION .
    RUNTIME DESTINATION ${CMAKE_INSTALL_BINDIR}
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
)

qt_generate_deploy_qml_app_script(
    TARGET apptab_qml
    OUTPUT_SCRIPT deploy_script
    MACOS_BUNDLE_POST_BUILD
    NO_UNSUPPORTED_PLATFORM_ERROR
    DEPLOY_USER_QML_MODULES_ON_UNSUPPORTED_PLATFORM
)
install(SCRIPT ${deploy_script})

