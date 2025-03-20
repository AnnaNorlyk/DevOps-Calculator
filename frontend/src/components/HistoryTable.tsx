import React from "react";
import { IHistoryRecord } from "../services/IHistoryRecord";
import "./HistoryTable.css";

// Shows the list of calculation history records in a table.
const HistoryTable: React.FC<{ history: IHistoryRecord[] }> = ({ history }) => {
  return (
    <div className="history-table-container">
      <h2>Calculation History</h2>
      <table className="history-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Operator</th>
            <th>First number</th>
            <th>Second number</th>
            <th>Result</th>
            <th>Timestamp</th>
          </tr>
        </thead>
        <tbody>
          {history.map((record) => (
            <tr key={record.id}>
              <td>{record.id}</td>
              <td>{record.operation}</td>
              <td>{record.operandA ?? "-"}</td>
              <td>{record.operandB ?? "-"}</td>
              <td>{record.result}</td>
              <td>{record.created_at}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default HistoryTable;

