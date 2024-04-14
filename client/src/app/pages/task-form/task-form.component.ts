import {
  Component,
  EventEmitter,
  Input,
  OnChanges,
  Output,
} from '@angular/core';
import { ITask } from '../shared/models/Task';
import {
  FormGroup,
  FormBuilder,
  FormControl,
  Validators,
  ReactiveFormsModule,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './task-form.component.html',
  styleUrl: './task-form.component.scss'
})
export class TaskFormComponent implements OnChanges {
    @Input() data: ITask | null = null;
    @Output() onCloseModal = new EventEmitter();

    taskForm!: FormGroup;

  constructor(
    private fb: FormBuilder,
    private taskService: TaskService,
    private toastr: ToastrService
  ) {
    this.taskForm = this.fb.group({
      description: new FormControl('', [Validators.required])
    });
  }

  onClose() {
    this.onCloseModal.emit(false);
  }

  ngOnChanges(): void {
    if (this.data) {
      this.taskForm.patchValue({
        description: this.data.description
      });
    }
  }

  onSubmit() {
    if (this.taskForm.valid) {
      if (this.data) {
        this.taskService
          .updateTaskItem(this.data.id as string, this.taskForm.value)
          .subscribe({
            next: (response: any) => {
              this.resetTaskForm();
              this.toastr.success("Task Updated");
            },
          });
      } else {
        this.taskService.addTaskItem(this.taskForm.value).subscribe({
          next: (response: any) => {
            this.resetTaskForm();
            this.toastr.success("Task Added");
          },
        });
      }
    } else {
      this.taskForm.markAllAsTouched();
    }
  }

  resetTaskForm() {
    this.taskForm.reset();
    this.onClose();
  }
}