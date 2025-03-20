import React, { useState, useEffect } from "react";
import CalculatorForm from "./components/CalculatorForm";
import HistoryTable from "./components/HistoryTable";
import { IHistoryRecord } from "./services/IHistoryRecord";
import { fetchHistory } from "./services/CalculatorService";
import "./App.css";

const App: React.FC = () => {
  const [calcType, setCalcType] = useState<"simple" | "cached">("simple");
  const [result, setResult] = useState("");
  const [history, setHistory] = useState<IHistoryRecord[]>([]);

  const updateHistory = async () => {
    try {
      const data = await fetchHistory();
      setHistory(data);
    } catch (err) {
      console.error("Error fetching history:", err);
    }
  };

  useEffect(() => {
    updateHistory();
  }, [calcType]);

  return (
    <div className="app-container">
      <header>
        <h1>Calculator</h1>
      </header>

      <section className="settings">
        <label htmlFor="calc-type" className="select-label">Calculator:</label>
        <select
          id="calc-type"
          className="select-input"
          value={calcType}
          onChange={(e) => setCalcType(e.target.value as "simple" | "cached")}
        >
          <option value="simple">Simple</option>
          <option value="cached">Cached</option>
        </select>
      </section>

      <section className="calculator-section">
        <CalculatorForm
          calculatorType={calcType}
          onOperationCompleted={setResult}
          onHistoryRefresh={updateHistory}
        />
        <div className="result-display">
          <h2>Result: {result}</h2>
        </div>
      </section>

      <section className="history-section">
        <HistoryTable history={history} />
      </section>
    </div>
  );
};

export default App;
