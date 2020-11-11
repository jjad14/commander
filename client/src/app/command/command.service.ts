import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Subject } from 'rxjs';

import { environment } from '../../environments/environment';
import { ICommand } from '../shared/models/command';
import { IPlatform } from '../shared/models/platform';
import { CommandParams } from '../shared/models/commandParams';
import { IPagination, Pagination } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class CommandService {
  apiUrl = environment.apiUrl;

  commands: ICommand[] = [];
  private commandsUpdated = new Subject<{commands: ICommand[], count: number, pageSize: number, pageNumber: number}>();

  pagination = new Pagination();
  commandParams = new CommandParams();

  platforms: IPlatform[] = [];

  constructor(private http: HttpClient) { }


  getCommands() {
    let params = new HttpParams();

    if (this.commandParams.platformId && this.commandParams.platformId !== 0) {
      params = params.append('platformId', this.commandParams.platformId.toString());
    }

    params = params.append('pageIndex', this.commandParams.pageNumber.toString());
    params = params.append('pageSize', this.commandParams.pageSize.toString());

    this.http
    .get<IPagination>(this.apiUrl + 'commands', {observe: 'response', params})
    .pipe(
      map(commandData => {
          return {
              commands: commandData.body.data.map(c => {
              return {
                  id: c.id,
                  task: c.task,
                  instructions: c.instructions,
                  platformId: c.platformId,
                  platformName: c.platformName
                };
              }),
              totalCount: commandData.body.count,
              pageSize: commandData.body.pageSize,
              pageNumber: commandData.body.pageIndex
      };
  }))
  .subscribe((transformedData) => {
      this.commands = transformedData.commands;
      this.commandsUpdated.next({
        commands: [...this.commands],
        count: transformedData.totalCount,
        pageSize: transformedData.pageSize,
        pageNumber: transformedData.pageNumber
      });
  });

  }

  getCommandById(id: number) {
    return this.http.get<ICommand>(this.apiUrl + 'commands/' + id);
    // return  this.commands.find(p => p.id === id);
  }


  createCommand(command: any) {
    this.http.post(this.apiUrl + 'commands', command)
      .subscribe(res => {
        console.log(res);
      });
  }

  updateCommand(id: number, command: any) {
    this.http.put(this.apiUrl + 'commands/' + id, command)
      .subscribe(res => {
        console.log(res);
      });
  }

  deleteCommand(id: number) {
    this.http.delete(this.apiUrl + 'commands/' + id )
      .subscribe(res => {
        this.commands = this.commands.filter(c => c.id !== id);
        // TODO: Fix subject params
        this.commandsUpdated.next({
          commands: [...this.commands],
          count: this.commands.length,
          pageSize: this.commandParams.pageSize,
          pageNumber: this.commandParams.pageNumber
        });
      });
  }

  // return commandsUpdated as an observable to be subscirbed
  getCommandUpdate() {
    return this.commandsUpdated.asObservable();
  }

  setCommandParams(params: CommandParams) {
    this.commandParams = params;
    // console.log(this.commandParams);
  }

  getCommandParams() {
    return this.commandParams;
  }

  getPlatforms() {
    return this.http.get<IPlatform[]>(this.apiUrl + 'commands/platforms')
    .pipe(
      map(res => {
        this.platforms = [...res];
        return this.platforms;
      })
    );
  }


}
