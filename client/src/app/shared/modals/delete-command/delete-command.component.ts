import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CommandService } from 'src/app/command/command.service';

export interface DialogData {
  id: number;
  task: string;
  instructions: string;
}

@Component({
  selector: 'app-delete-command',
  templateUrl: './delete-command.component.html',
  styleUrls: ['./delete-command.component.css']
})
export class DeleteCommandComponent implements OnInit {

  constructor(
    private commandService: CommandService,
    public dialogRef: MatDialogRef<DeleteCommandComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {}


  ngOnInit(): void {
  }

  onYesClick(id: number) {
    this.commandService.deleteCommand(id);

    this.dialogRef.close();
  }

  onNoClick() {
    this.dialogRef.close();
  }

}
