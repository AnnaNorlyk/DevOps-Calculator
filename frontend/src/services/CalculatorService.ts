import { BASE_URL } from "./config";
import { IHistoryRecord } from "./IHistoryRecord";

export async function doCalculation(
  operation: string,
  calculatorType: "simple" | "cached",
  a: number,
  b?: number
): Promise<string> {
  let url: string;

  // Check if we need two operands
  if (["add", "subtract", "multiply", "divide"].includes(operation)) {
    if (b === undefined) {
      throw new Error("You must provide 'b' for this operation.");
    }


    url = `${BASE_URL}/api/${calculatorType}/${operation}?a=${a}&b=${b}`;
  } else {
    url = `${BASE_URL}/api/${calculatorType}/${operation}?a=${a}`;
  }

  const response = await fetch(url);
  if (!response.ok) {
    throw new Error("Calculation failed: " + response.statusText);
  }

  const data = await response.json();

  // If the operation is prime, the API returns true/false.
  if (operation === "prime") {
    return data ? "Yes" : "No";
  }

  return String(data);
}

export async function fetchHistory(): Promise<IHistoryRecord[]> {
  const response = await fetch(`${BASE_URL}/api/history`);
  if (!response.ok) {
    throw new Error("Could not get history");
  }
  return response.json();
}