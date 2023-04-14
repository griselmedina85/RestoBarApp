import { Person } from './personclass.models'

export class Client {

    private constructor(
        public personLastName: string = '',
        public personName: string = '',
        public email: string = '',
        public phone: string = '',
        public personId: number = 0
    ) { }

    static createBlank(): Client {
        return new Client();
    }

    static createClient(person: Person) {
        return new Client(person.personLastName, person.personName, person.email, person.phone, person.personId);
    }

}
