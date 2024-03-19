import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../../_services/admin.service';
import { ArtworkAdminDTO } from 'src/app/_model/artworkAdminDTO.model';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-artwork-list',
  templateUrl: './artwork-list.component.html',
  styleUrls: ['./artwork-list.component.css']
})
export class ArtworkListComponent implements OnInit {
  artworks: ArtworkAdminDTO[] = [];

  constructor(private adminService: AdminService) { }

  ngOnInit(): void {
    this.loadArtworks();
  }

  loadArtworks() {
    this.adminService.getAllArtworks().subscribe({
      next: (artworks) => {
        this.artworks = artworks;
      },
      error: (error) => {
        console.error('There was an error!', error);
      }
    });
  }

  deleteArtwork(artworkId: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: "You won't be able to revert this!",
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#3085d6',
      cancelButtonColor: '#d33',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.adminService.deleteArtwork(artworkId).subscribe({
          next: () => {
            Swal.fire(
              'Deleted!',
              'The artwork has been deleted.',
              'success'
            );
            // Refresh the artworks list
            this.loadArtworks();
          },
          error: (error) => {
            Swal.fire(
              'Failed!',
              'There was an error deleting the artwork.',
              'error'
            );
          }
        });
      }
    });
  }
}
