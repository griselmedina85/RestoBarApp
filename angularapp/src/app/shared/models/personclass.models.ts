import { FormGroup } from "@angular/forms";

export class Person {

    private constructor(
        public personId: number = 0,
        public personLastName: string = '',
        public personName: string = '',
        public identityNumber: number = 0,
        public birthday: Date = new Date(),
        public email: string = '',
        public phone: string = '',
        public gender: string = '',
        public client: any = null
    ) { }

    static clonePerson(person: Person): Person {
        return new Person(person.personId, person.personLastName, person.personName, person.identityNumber, person.birthday, person.email, person.phone, person.gender, person.client)
    }

    static createBlank(): Person {
        return new Person();
    }

    static createPerson(valores: { form: FormGroup, id: number }): Person {
        return new Person(
            valores.id, 
            valores.form.value.personLastName,
            valores.form.value.personName,
            valores.form.value.identityNumber,
            valores.form.value.birthday,
            valores.form.value.email,
            valores.form.value.phone,
            valores.form.value.gender,
            null
            );
    }


}