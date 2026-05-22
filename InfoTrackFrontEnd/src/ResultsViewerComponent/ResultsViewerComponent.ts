import { Component, DestroyRef, inject, OnDestroy, OnInit } from "@angular/core";
import { SolicitorResultsService } from "../SolicitorResults/SolicitorResultsService";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { FormControl, ReactiveFormsModule } from "@angular/forms";
import { SolicitorResultsDto } from "../SolicitorResults/solicitor-results-types";
import { BehaviorSubject } from "rxjs";
import { AsyncPipe} from "@angular/common";

@Component({
    selector: "results-viewer",
    templateUrl: "./ResultsViewerComponent.html",
    styleUrl: "./ResultsViewerComponent.css",
    imports: [ReactiveFormsModule, AsyncPipe]
})
export class ResultsViewerComponent {
    public locations: string[] = ["Location 1", "Location 2", "Location 3"]; 
    public currentResultsLocation = "";  
    public locationsControl = new FormControl<string>(this.locations[0]);
    public results: BehaviorSubject<SolicitorResultsDto | null> = new BehaviorSubject<SolicitorResultsDto | null>(null);

    private readonly _destroyRef = inject(DestroyRef);
    
    constructor(private _resultsService: SolicitorResultsService){}

    public ViewResults(): void {
        if(!!this.locationsControl.value) {
            this._resultsService.GetSolicitorResults(this.locationsControl.value).pipe(takeUntilDestroyed(this._destroyRef)).subscribe(resultVal => {
                console.log(JSON.stringify(resultVal));
                this.currentResultsLocation = this.locationsControl.value ?? "";
                this.results.next(resultVal);
            });
        }
    }   
}