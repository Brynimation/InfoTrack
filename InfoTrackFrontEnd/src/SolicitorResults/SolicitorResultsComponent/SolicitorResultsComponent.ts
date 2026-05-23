import { Component, Input } from "@angular/core";
import { SolicitorResultsResponseDto } from "../solicitor-results-types";
import { KeyValuePipe } from "@angular/common";

@Component({
    selector: "solicitor-results",
    templateUrl: "./SolicitorResultsComponent.html",
    styleUrl: "./SolicitorResultsComponent.css",
    imports: [KeyValuePipe]
})
export class SolicitorResultsComponent {
    @Input()
    public data: SolicitorResultsResponseDto | null = null;

    protected convertCamelCaseToDisplay(str: string): string {
        return str.replace(/([A-Z])/g, " $1").replace(/^./, function (txt) {
            return txt.toUpperCase();
        });
    }
}