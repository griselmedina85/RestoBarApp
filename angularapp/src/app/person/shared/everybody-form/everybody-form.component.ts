import { ChangeDetectionStrategy, Component, EventEmitter, Input, OnChanges, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Person } from 'src/app/shared/models/personclass.models';
import { EverybodyService } from '../everybody.service';


@Component({
  selector: 'app-everybody-form',
  templateUrl: './everybody-form.component.html',
  styleUrls: ['./everybody-form.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EverybodyFormComponent implements OnInit, OnChanges {

  @Input() vm!: Person;

  @Output() SaveVm = new EventEmitter<{form: FormGroup, id: number}>();
  @Output() DeleteVm = new EventEmitter<number>();
  @Output() ChangePerson = new EventEmitter<boolean>();

  buttonOn$ = this.everybodyService.enableButtonSubject$;

  loading = false;

  formPerson = this.fb.group({
    personName: new FormControl('',[Validators.required, Validators.minLength(3)]),
    personLastName: new FormControl('',[Validators.required, Validators.minLength(3)]),
    identityNumber: new FormControl(0, [Validators.required, Validators.min(999999)]),
    gender: new FormControl('',[Validators.required]),
    phone: new FormControl('',[Validators.required]),
    birthday: new FormControl(new Date(),[]),
    email: new FormControl('',[Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$")]),
    isClient: new FormControl(true,[]),
    isUser: new FormControl(false,[]),
  });

  constructor(
    private readonly fb: FormBuilder,
    public everybodyService: EverybodyService,
    ) { }

    ngOnInit(): void {
    }


  ngOnChanges(): void {
    this.UpdateData();
  }

  recibedato(dato: Date){
    this.formPerson.patchValue({ birthday: dato });
  }

  onSave(): void {
    this.everybodyService.ButtonOff();
    const valores = {
      form: this.formPerson, 
      id: this.vm.personId
    };

    this.SaveVm.emit(valores);
}

  onCancel(): void {
    this.UpdateData();
    this.everybodyService.ButtonOff();
  }

  deleteClient(): void {
    this.DeleteVm.emit(this.vm.personId)

  }
  
  // getters
  get personName(){
    return this.formPerson.get('personName');
  }

  get personLastName(){
    return this.formPerson.get('personLastName');
  }

  get identityNumber(){
    return this.formPerson.get('identityNumber');
  }
  get gender(){
    return this.formPerson.get('gender');
  }
  get phone(){
    return this.formPerson.get('phone');
  }

  get email(){
    return this.formPerson.get('email');
  }


  UpdateData(): void {
    if (this.vm){
      this.formPerson.patchValue(this.vm);
    }   
  }


}
