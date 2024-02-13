import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Image } from '../_models/Image';

@Injectable({
  providedIn: 'root'
})
export class UploadService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) {}
  uploadImage(imageFile: File) {
    const formData = new FormData();
    formData.append('file', imageFile);
      return this.http.post<Image>(this.baseUrl+'upload',formData).pipe(
        
        catchError((err: any) => {
          console.error('Interceptor Error:', err);
          
          return throwError(() => new Error('Undefined error occurred'));
        })
      );
  
}
}
