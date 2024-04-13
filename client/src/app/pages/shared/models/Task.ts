export interface ApiResponse<T> {
  message?: string;
  data: T;
}

export interface ITask {
  id: string;
  description: string;

}