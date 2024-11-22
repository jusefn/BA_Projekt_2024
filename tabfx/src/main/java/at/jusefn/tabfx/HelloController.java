package at.jusefn.tabfx;

import javafx.fxml.FXML;
import javafx.scene.control.*;
import java.io.IOException;


public class HelloController {

    @FXML
    private TextField calcField;

    @FXML
    private TextArea editField;
    
    Calculator calculator = new Calculator();
    Editor editor = new Editor();

    // Editor

    @FXML
    protected void onNewButtonClick() {
        editor.newFile(editField);
    }

    @FXML
    protected void onLoadButtonClick() throws IOException {
        editor.loadFile(editField);
    }

    @FXML
    protected void onSaveButtonClick() throws IOException {
        editor.saveFile(editField);
    }

    // Calculator

    @FXML
    protected void onZeroClick() {
        calculator.enter("0", calcField);
    }
    @FXML
    protected void onOneClick() {
        calculator.enter("1", calcField);
    }
    @FXML
    protected void onTwoClick() {
        calculator.enter("2", calcField);
    }
    @FXML
    protected void onThreeClick() {
        calculator.enter("3", calcField);
    }
    @FXML
    protected void onFourClick() {
        calculator.enter("4", calcField);
    }
    @FXML
    protected void onFiveClick() {
        calculator.enter("5", calcField);
    }
    @FXML
    protected void onSixClick() {
        calculator.enter("6", calcField);
    }
    @FXML
    protected void onSevenClick() {
        calculator.enter("7", calcField);
    }
    @FXML
    protected void onEightClick() {
        calculator.enter("8", calcField);
    }
    @FXML
    protected void onNineClick() {
        calculator.enter("9", calcField);
    }
    @FXML
    protected void onPlusClick() {
        calculator.enter("+", calcField);
    }
    @FXML
    protected void onMinusClick() {
        calculator.enter("-", calcField);
    }
    @FXML
    protected void onMultiplyClick() {
        calculator.enter("*", calcField);
    }
    @FXML
    protected void onDivideClick() {
        calculator.enter("/", calcField);
    }
    @FXML
    protected void onEqualClick() {
        calculator.enter("=", calcField);
    }


}