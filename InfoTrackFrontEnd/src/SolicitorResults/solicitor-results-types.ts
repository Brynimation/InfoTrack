export class SolicitorResultsResponseDto {
    public name: string;
    public address: string;
    public phoneNumber: string;
    
    constructor(name: string, address: string, phoneNumber: string) {
        this.name = name;
        this.address = address;
        this.phoneNumber = phoneNumber;
    }
}