import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormGroupDirective, NgForm, FormBuilder } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { CommandService } from '../command.service';
import { IPlatform } from 'src/app/shared/models/platform';
import { ICommand } from 'src/app/shared/models/command';

/** Error when invalid control is dirty, touched, or submitted. */
export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-create-command',
  templateUrl: './create-command.component.html',
  styleUrls: ['./create-command.component.css']
})
export class CreateCommandComponent implements OnInit {
  enteredTask = '';
  enteredInstructions = '';
  title = '';
  button = '';
  isLoading = false;
  submitted = false;

  private mode = 'create';
  private commandId: string;

  form: FormGroup;

  command: ICommand;
  platforms: IPlatform[] = [];
  matcher = new MyErrorStateMatcher();

  constructor(private commandService: CommandService, private fb: FormBuilder, public route: ActivatedRoute) { }

  ngOnInit(): void {
    this.createCommandForm();
    this.getPlatforms();
    this.checkMode();
  }

  checkMode() {
        // check if user is creating a command or editing an existing command
        this.route.paramMap.subscribe((paramMap: ParamMap) => {
          // if their is a command id is in the url (params) then its in edit mode
          if (paramMap.has('commandId')) {
              this.mode = 'edit';
              this.title = 'Edit Command';
              this.button = 'Edit';
              this.commandId = paramMap.get('commandId');
              // this.isLoading = true;

              // get command by the command id
              this.commandService.getCommandById(+this.commandId)
                  .subscribe(commandData => {
                      // this.isLoading = false;
                      this.command = {
                                  id: commandData.id,
                                  task: commandData.task,
                                  instructions: commandData.instructions,
                                  platformId: commandData.platformId,
                                  platformName: commandData.platformName
                      };

                      // insert command data onto form fields
                      this.form.setValue({
                        task: this.command.task,
                        instructions: this.command.instructions,
                        platform: this.command.platformId
                      });
                  });
          // create mode
          } else {
              this.mode = 'create';
              this.title = 'Create Command';
              this.button = 'Create';
              this.commandId = null;
          }
      });
  }

  createCommandForm() {
    this.form = this.fb.group({
      task: ['', [Validators.required]],
      instructions: ['', [Validators.required]],
      platform: ['', [Validators.required]]
    });
  }

  onSubmit(formData: any, formDirective: FormGroupDirective) {
    this.submitted = true;

    if (this.form.invalid) {
        return;
    }
    // this.isLoading = true;

    if (this.mode === 'create') {
      const data = {
          task: this.form.value.task,
          instructions: this.form.value.instructions,
          platformId: +this.form.value.platform,
          platformName: this.platforms.filter(p => p.id === +this.form.value.platform)[0].name
        };

      this.commandService.createCommand(data);

      formDirective.resetForm();
      this.form.reset();

      // Toaster success
    } else {
      const data = {
        task: this.form.value.task,
        instructions: this.form.value.instructions,
        platformId: +this.form.value.platform,
        platformName: this.platforms.filter(p => p.id === +this.form.value.platform)[0].name
      };

      console.log(data);

      this.commandService.updateCommand(+this.commandId, data);
      // Toaster success
    }
  }

  getPlatforms() {
    this.commandService.getPlatforms()
      .subscribe(res => {
        this.platforms = res;
      }, error => {
        console.log(error);
        // Toaster error
      });
  }

}
