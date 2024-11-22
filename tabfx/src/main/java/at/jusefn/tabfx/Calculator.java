package at.jusefn.tabfx;

import javafx.scene.control.Alert;
import javafx.scene.control.TextField;


public class Calculator {

    private String numberOne = "";
    private String numberTwo = "";
    private String operator = "";

    public void enter(String number, TextField calcField) {
        if(number.equals("+") || number.equals("-") || number.equals("*") || number.equals("/")) {
            if(numberOne.isEmpty()) {
                Alert alert = new Alert(Alert.AlertType.INFORMATION);
                alert.setTitle("Error");
                alert.setHeaderText(null);
                alert.setContentText("Please enter a number");
                alert.showAndWait();
            }
            else {
                operator = number;
                calcField.setText(numberOne + operator);
            }
        } else if (number.equals("=")) {
            long realNumberOne = Long.parseLong(numberOne);
            long realNumberTwo = Long.parseLong(numberTwo);
            Long result = switch (operator) {
                case "+" -> realNumberOne + realNumberTwo;
                case "-" -> realNumberOne - realNumberTwo;
                case "*" -> realNumberOne * realNumberTwo;
                case "/" -> realNumberOne / realNumberTwo;
                default -> 0L;
            };
            calcField.setText(numberOne + operator + numberTwo + "=" + result);
            numberOne = result.toString();
        }
        else {
            if(operator.isEmpty()) {
                numberOne = number;
                calcField.setText(numberOne);
            }
            else {
                numberTwo = number;
                calcField.setText(numberOne + operator + numberTwo);
            }
        }

    }
}
