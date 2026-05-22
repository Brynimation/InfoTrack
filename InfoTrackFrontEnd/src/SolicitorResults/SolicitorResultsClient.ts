import { Injectable } from "@angular/core";
import { SolicitorResultsDto as SolicitorResultsResponseDto } from "./solicitor-results-types";
import { Observable } from "rxjs/internal/Observable";
import { HttpClient } from "@angular/common/http";

@Injectable({
    providedIn: 'root' 
})
export class SolicitorResultsClient {
    private baseUrl: string = "https://localhost:7215";
    constructor(private _httpClient: HttpClient) {

    }

    public getSolicitorResults(location: string): Observable<SolicitorResultsResponseDto> {
        const url: string = `${this.baseUrl}/SolicitorResults?location=${location}`;
        return this._httpClient.get<SolicitorResultsResponseDto>(url);
    }   
 }