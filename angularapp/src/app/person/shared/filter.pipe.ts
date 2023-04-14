import { Pipe, PipeTransform } from '@angular/core';
import { Person } from 'src/app/shared/models/personclass.models';

@Pipe({
  name: 'filterPipe',
  pure: false,
})
export class FilterPipe implements PipeTransform {

  transform(value: Person[], text: string): Person[] {
    if (!text || text.trim() === '') {
      return value;
    } else {
      return value.filter((person) => {
        const term = text.toUpperCase();
        return (
          person.personName.toUpperCase().includes(term) ||
          person.personLastName.toUpperCase().includes(term) ||
          person.identityNumber.toString().includes(term)
        );
      });
    }
  }
}
