import { ValidatorFn, FormGroup } from '@angular/forms';

/** Password and Confirm Password must match */
export function passwordMatchValidator(g: FormGroup): boolean {
    return g.get('password').value === g.get('confirmPassword').value ? true : false;
}
