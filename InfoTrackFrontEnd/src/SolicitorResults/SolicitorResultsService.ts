import { Injectable } from "@angular/core";
import { SolicitorResultsClient } from "./SolicitorResultsClient";
import { SolicitorResultsDto } from "./solicitor-results-types";
import { Observable } from "rxjs/internal/Observable";
import { Location } from "@angular/common";

@Injectable({
    providedIn: 'root' 
})
export class SolicitorResultsService {

    constructor(private client: SolicitorResultsClient) {
        
    }
    public GetSolicitorResults(location: string): Observable<SolicitorResultsDto> {
        return this.client.getSolicitorResults(location);
    }
}