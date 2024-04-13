import { Component, OnInit } from '@angular/core';
import { ModelComponent } from '../shared/components/model.component'; 
import { TaskFormComponent } from '../task-form/task-form.component';
import { ToastrService } from 'ngx-toastr';
import { TaskService } from '../../services/task.service';
import { ITask } from '../shared/models/Task';

@Component({
  selector: 'app-task',
  standalone: true,
  imports: [ModelComponent, TaskFormComponent],
  templateUrl: './task.component.html',
  styleUrl: './task.component.scss'
})
export class TaskComponent implements OnInit {
  isModelOpen = false;
  tasks: ITask[] = [];
  task!: ITask;

  constructor(
    private taskService: TaskService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getTaskItems();
  }

  getTaskItems() {
    this.taskService.getTaskItems().subscribe({
      next: (response) => {
        if (response.data) {
          this.tasks = response.data;
        }
      },
    });
  }

  loadTaskItem(task: ITask) {
    this.task = task;
    this.openModel();
  }

  deleteTaskItem(id: string) {
    this.taskService.deleteTaskItem(id).subscribe({
      next: (response) => {
        this.toastr.success(response.message);
        this.getTaskItems();
      },
    });
  }

  openModel() {
    this.isModelOpen = true;
  }

  closeModel() {
    this.isModelOpen = false;
    this.getTaskItems();
  }
}