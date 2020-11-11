import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from '../shared/shared.module';
import { CreateCommandComponent } from './create-command/create-command.component';
import { ListCommandComponent } from './list-command/list-command.component';
import { CommandRoutingModule } from './command-routing.module';


@NgModule({
  declarations: [CreateCommandComponent, ListCommandComponent],
  imports: [
    CommonModule,
    SharedModule,
    CommandRoutingModule
  ]
})
export class CommandModule { }
