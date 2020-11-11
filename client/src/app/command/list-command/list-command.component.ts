import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Subscription } from 'rxjs';

import { CommandService } from '../command.service';
import { ICommand } from '../../../app/shared/models/command';
import { IPlatform } from '../../../app/shared/models/platform';
import { DeleteCommandComponent } from '../../../app/shared/modals/delete-command/delete-command.component';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-list-command',
  templateUrl: './list-command.component.html',
  styleUrls: ['./list-command.component.css']
})
export class ListCommandComponent implements OnInit, OnDestroy {
  commands: ICommand[] = [];
  platforms: IPlatform[] = [];

  totalPosts = 0;
  postsPerPage = 1;
  currentPage = 1;
  pageSizeOptions = [1, 2, 5, 10];

  commandsSubscription: Subscription;

  constructor(private commandService: CommandService, public dialog: MatDialog) { }

  ngOnInit() {
    this.getCommands();
    this.getPlatforms();
  }

  getCommands() {
    this.commandService.getCommands();

    this.commandsSubscription = this.commandService
      .getCommandUpdate()
      .subscribe((commandData: {commands: ICommand[], count: number, pageSize: number, pageNumber: number}) => {
              // this.isLoading = false;
              this.commands = commandData.commands;
              this.totalPosts = commandData.count;
              this.postsPerPage = commandData.pageSize;
              this.currentPage = commandData.pageNumber;
      });
  }

  getPlatforms() {
    this.commandService.getPlatforms()
      .subscribe(res => {
        this.platforms = res;
      });
  }

  filterByPlatform(id: number) {
    const params = this.commandService.getCommandParams();

    params.platformId = id;
    params.pageNumber = 1;

    this.commandService.setCommandParams(params);
    this.getCommands();
  }

  searchCommands() {
    // Todo
  }

  onChangedPage(pageData: PageEvent) {
    const params = this.commandService.getCommandParams();

    params.pageNumber = pageData.pageIndex + 1;
    params.pageSize = pageData.pageSize;

    this.currentPage = pageData.pageIndex + 1;
    this.postsPerPage = pageData.pageSize;

    console.log(pageData);

    this.commandService.setCommandParams(params);
    this.getCommands();
  }

  onDelete(cmd: ICommand) {
      const dialogRef = this.dialog.open(DeleteCommandComponent, {
        width: '400px',
        data: {id: cmd.id, task: cmd.task, instructions: cmd.instructions}
      });

      dialogRef.afterClosed().subscribe(result => {
        console.log('The dialog was closed');
      });

      this.commandService.getCommands();
  }

  ngOnDestroy() {
    this.commandsSubscription.unsubscribe();
  }

}
