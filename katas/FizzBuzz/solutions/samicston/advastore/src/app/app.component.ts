import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'advastore';

  isMultipleOf = (m: number) => (n: number): boolean => n % m === 0;

  internal(n: number): string {
    let result = "";

    if (this.isMultipleOf(3)(n)) {
      result += "Fizz";
    }

    if (this.isMultipleOf(5)(n)) {
      result += "Buzz";
    }

    return result || n.toString();
  }

  FizzBuzz(n: number): string[] {
    return [...Array(n)].map((_, i) => i + 1).map((j) => this.internal(j));
  }
}
