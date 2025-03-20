export interface CalculatorFormProps {
    calculatorType: "simple" | "cached";
  // Callback when an operation is completed (to show the result)
  onOperationCompleted: (result: string) => void;
  
  // Callback to refresh the calculation history
  onHistoryRefresh: () => void;
}
