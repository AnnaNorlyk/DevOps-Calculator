export interface IHistoryRecord {
    id: number;
    operation: string;
    operandA: number | null;
    operandB: number | null;
    result: string;
    created_at: string;
  }
  