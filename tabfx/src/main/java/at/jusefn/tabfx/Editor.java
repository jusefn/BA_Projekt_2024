package at.jusefn.tabfx;

import javafx.scene.control.TextArea;
import javafx.stage.FileChooser;
import javafx.stage.Stage;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.nio.file.Files;

public class Editor {

    FileChooser fileChooser = new FileChooser();

    public void newFile(TextArea editField) {
        editField.clear();
    }


    public void loadFile(TextArea editField) throws IOException {
        Stage stage = new Stage();
        stage.setTitle("Select File");

        // get the file selected
        File file = fileChooser.showOpenDialog(stage);

        if (file != null) {
            String text = new String(Files.readAllBytes(file.toPath()));
            editField.setText(text);
        }
    }

    public void saveFile(TextArea editField) throws IOException {
        Stage stage = new Stage();
        stage.setTitle("Save File");
        BufferedWriter writer;

        // get the file selected
        File file = fileChooser.showSaveDialog(stage);
        try {
            file.createNewFile();
            writer = new BufferedWriter(new FileWriter(file.getAbsoluteFile()));
            writer.write(editField.getText());
            writer.close();
        } catch (IOException e) {
            throw new RuntimeException(e);
        }
    }
}
