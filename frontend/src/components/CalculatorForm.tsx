import React, { useState } from "react";
import { doCalculation } from "../services/CalculatorService";
import { CalculatorFormProps } from "./ICalculatorFormProps";
import "./CalculatorForm.css";


const CalculatorForm: React.FC<CalculatorFormProps> = ({
  calculatorType,
  onOperationCompleted,
  onHistoryRefresh,
}) => {
  const [operation, setOperation] = useState("add");
  const [first, setFirst] = useState(0);
  const [second, setSecond] = useState(0);

  const handleCalculate = async (operation: string, a: number, b?: number) => {
    try {
      const result = await doCalculation(operation, calculatorType, a, b);
      onOperationCompleted(result);
      onHistoryRefresh(); // Refresh history after calculation
    } catch (error) {
      console.error("Calculation error:", error instanceof Error ? error.message : error);
    }
  };
  

  return (
    <div className="calculator-form">
      {/* Choose an operation */}
      <div className="form-group">
        <label>Operation:</label>
        <select value={operation} onChange={(e) => setOperation(e.target.value)}>
          <option value="add">Add</option>
          <option value="subtract">Subtract</option>
          <option value="multiply">Multiply</option>
          <option value="divide">Divide</option>
          <option value="factorial">Factorial</option>
          <option value="isprime">Is Prime</option>
        </select>
      </div>

      {/* Input for the first number */}
      <div className="form-group">
        <label>First:</label>
        <input
          type="number"
          value={first}
          onChange={(e) => setFirst(Number(e.target.value))}
        />
      </div>

      {/* Only show second input if the operation needs two numbers */}
      {["add", "subtract", "multiply", "divide"].includes(operation) && (
        <div className="form-group">
          <label>Second:</label>
          <input
            type="number"
            value={second}
            onChange={(e) => setSecond(Number(e.target.value))}
          />
        </div>
      )}

      {/* Button to calculate */}
      <button
      className="calculate-btn"
      onClick={() => handleCalculate(
      operation, 
      first, 
      ["add", "subtract", "multiply", "divide"].includes(operation) ? second : undefined
    )}
  >
    Calculate
  </button>

    </div>
  );
};

export default CalculatorForm;