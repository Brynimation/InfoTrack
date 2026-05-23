import { Component, DestroyRef, inject, OnDestroy, OnInit } from "@angular/core";
import { SolicitorResultsService } from "../SolicitorResults/SolicitorResultsService";
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { FormControl, ReactiveFormsModule } from "@angular/forms";
import { SolicitorResultsResponseDto } from "../SolicitorResults/solicitor-results-types";
import { BehaviorSubject } from "rxjs";
import { AsyncPipe, JsonPipe} from "@angular/common";
import { SolicitorResultsComponent } from "../SolicitorResults/SolicitorResultsComponent/SolicitorResultsComponent";

@Component({
    selector: "results-viewer",
    templateUrl: "./ResultsViewerComponent.html",
    styleUrl: "./ResultsViewerComponent.css",
    imports: [ReactiveFormsModule, AsyncPipe, SolicitorResultsComponent]
})
export class ResultsViewerComponent {
    public locations: string[] = ["London", "Birmingham", "Manchester", "Liverpool", "Leeds", "Sheffield", "Bristol", "Bradford"]; 
    public currentResultsLocation = "";
    public loadingText = "";  
    public locationsControl = new FormControl<string>(this.locations[0]);
    public results: BehaviorSubject<SolicitorResultsResponseDto[] | null> = new BehaviorSubject<SolicitorResultsResponseDto[] | null>(null);

    private readonly _destroyRef = inject(DestroyRef);
    
    constructor(private _resultsService: SolicitorResultsService){}

    public ViewResults(): void {
        if(!!this.locationsControl.value) {
            this.loadingText = "Loading...";
            this._resultsService.GetSolicitorResults(this.locationsControl.value).pipe(takeUntilDestroyed(this._destroyRef)).subscribe(resultVal => {
                this.loadingText = "";
                this.currentResultsLocation = this.locationsControl.value ?? "";
                this.results.next(resultVal);
            });
        }
    }   
}