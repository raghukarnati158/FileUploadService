import { Routes } from '@angular/router';
import { FileUpload, PageNotFound } from './components';

export const routes: Routes = [
    { path: 'fileupload', component: FileUpload },
    { path: '',   redirectTo: '/fileupload', pathMatch: 'full' }, // redirect to `first-component`
    { path: '**', component: PageNotFound },  // Wildcard route for a 404 page
];
