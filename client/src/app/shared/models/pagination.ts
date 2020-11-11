import { ICommand } from './command';

export interface IPagination {
    pageIndex: number;
    pageSize: number;
    count: number;
    data: ICommand[];
  }

export class Pagination implements IPagination {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: ICommand[] = [];
}
