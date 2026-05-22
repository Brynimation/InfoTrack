import { Routes } from '@angular/router';
import { ResultsViewerComponent } from '../ResultsViewerComponent/ResultsViewerComponent';

export const routes: Routes = [
    { path: '', redirectTo: '/results-viewer', pathMatch: 'full' },
    { path: 'results-viewer', component: ResultsViewerComponent }
];
