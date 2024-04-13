import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse, ITask } from '../pages/shared/models/Task';

@Injectable({
  providedIn: 'root',
})
export class TaskService {
  apiUrl = 'http://localhost:5001/api/tasks';
  constructor(private http: HttpClient) {}


  getTaskItems(): Observable<ITask[]> {
    return this.http.get<ITask[]>(`${this.apiUrl}`);
  }

  getTaskItem(id: string): Observable<ITask> {
    return this.http.get<ITask>(`${this.apiUrl}/${id}`);
  }

  addTaskItem(task: ITask): Observable<any> {
    return this.http.post(`${this.apiUrl}`, task);
  }

  updateTaskItem(id: string, task: ITask): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, task);
  }

  deleteTaskItem(id: string): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }
}


