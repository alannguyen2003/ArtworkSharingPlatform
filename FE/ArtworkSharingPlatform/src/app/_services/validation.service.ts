import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {
  constructor() { }

  minPriceValidation(minPrice: number): boolean {
    return minPrice > 0 && minPrice % 1000 == 0;
  }
  maxPriceValidation(maxPrice: number, minPrice: number): boolean {
    return maxPrice > 0 && maxPrice > minPrice && maxPrice % 1000 == 0;
  }
}
