import {CanActivateFn, CanDeactivateFn} from '@angular/router';
import {ProfileEditComponent} from "../components/profile-edit/profile-edit.component";
import {inject} from "@angular/core";
import {ConfirmService} from "../_services/confirm.service";

export const preventUnsavedChangesUserGuard:
  CanDeactivateFn<ProfileEditComponent> = (component) => {
  const confirmService = inject(ConfirmService);
  if (component.editForm?.dirty) {
    return confirmService.confirm();
  }
  return true;
};
