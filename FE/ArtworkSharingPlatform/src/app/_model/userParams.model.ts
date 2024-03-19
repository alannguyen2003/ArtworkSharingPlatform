export interface UserParams {
  orderBy: string;
  pageNumber: number ;
  pageSize: number ;
  rowSize: number ;
  minPrice: number;
  maxPrice: number;
  search: string;
  genreIds: number[];
}
