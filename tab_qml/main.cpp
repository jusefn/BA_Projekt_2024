#include <QGuiApplication>
#include <QQmlApplicationEngine>
#include <QFontDatabase>
#include <QDebug>
#include <QQmlContext>

int main(int argc, char *argv[])
{
    QGuiApplication::setApplicationName("GUIProject");
    QGuiApplication::setOrganizationName("QtProject");

    QGuiApplication app(argc, argv);

    if (QFontDatabase::addApplicationFont(":/fonts/fontello.ttf") == -1)
        qWarning() << "Failed to load fontello.ttf";

    QStringList selectors;
#ifdef QT_EXTRA_FILE_SELECTOR
    selectors += QT_EXTRA_FILE_SELECTOR;
#endif
    QQmlApplicationEngine engine;
    engine.setExtraFileSelectors(selectors);

    engine.loadFromModule("tab_qml", "Main");
    if (engine.rootObjects().isEmpty())
        return -1;

    return app.exec();


}
