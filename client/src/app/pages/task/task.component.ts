import { Component, OnInit } from '@angular/core';
import { ModalComponent } from '../shared/components/modal/modal.component';
import { TaskFormComponent } from '../task-form/task-form.component';
import { ToastrService } from 'ngx-toastr';
import { TaskService } from '../../services/task.service';
import { ITask } from '../shared/models/Task';

@Component({
  selector: 'app-task',
  standalone: true,
  imports: [ModalComponent, TaskFormComponent],
  templateUrl: './task.component.html',
  styleUrl: './task.component.scss',
})
export class TaskComponent implements OnInit {
  isModalOpen = false;
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
      next: (tasks) => {
        if (tasks) {
          this.tasks = tasks;
        }
      },
    });
  }

  loadTaskItem(task: ITask) {
    this.task = task;
    this.openModal();
  }

  deleteTaskItem(id: string) {
    this.taskService.deleteTaskItem(id).subscribe({
      next: (response) => {
        this.toastr.success(response.message);
        this.getTaskItems();
      },
    });
  }

  openModal() {
    this.isModalOpen = true;
  }

  closeModal() {
    this.isModalOpen = false;
    this.getTaskItems();
  }
}
