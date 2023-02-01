import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        AppComponent
      ],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'advastore'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('advastore');
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(AppComponent);
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.content span')?.textContent).toContain('advastore app is running!');
  });

  it(`should decide if N is multiple of 3 or 5`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.isMultipleOf(3)(15)).toEqual(true);
    expect(app.isMultipleOf(5)(15)).toEqual(true);
    expect(app.isMultipleOf(3)(25)).toEqual(false);
    expect(app.isMultipleOf(5)(25)).toEqual(true);
  })

  it('should print fizz or buzz or fizzbuzz for numbers that are multiple of 3 or/and 5', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.internal(15)).toEqual('FizzBuzz');
    expect(app.internal(9)).toEqual('Fizz');
    expect(app.internal(25)).toEqual('Buzz');
    expect(app.internal(20)).not.toEqual('20');
  })

  it('should check the array of numbers of if each one is a multiple of 3 or/and 5', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.FizzBuzz(5)).toEqual(['1', '2', 'Fizz', '4', 'Buzz']);
    expect(app.FizzBuzz(10)).toEqual(['1', '2', 'Fizz', '4', 'Buzz', 'Fizz', '7', '8', 'Fizz', 'Buzz']);
  })
});
