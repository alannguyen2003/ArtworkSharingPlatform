import { Injectable } from '@angular/core';
import {Artwork} from "../_model/artwork.model";
import {User} from "../_model/user.model";

@Injectable({
  providedIn: 'root'
})
export class ArtworkService {
  fakeUser: User = {
    name: 'Test',
    telephone: '0123456789',
    email: 'test@gmail.com',
    imageUrl: 'https://randomuser.me/api/portraits/men/2.jpg',
    role: 'Artist'
  } as User;
  artworks: Artwork[] = [
    {
      id:1,
      title: 'A portrait',
      description: "Pariatur cillum exercitation veniam et nostrud cupidatat do. Proident mollit amet nulla laboris ut occaecat veniam nulla fugiat. Enim ex commodo minim eiusmod incididunt magna velit anim. Incididunt sint eiusmod culpa labore non nulla dolore officia. Reprehenderit ex et ut ullamco proident non. Consectetur pariatur dolor ut aliquip est labore qui non ut cupidatat. Laborum et Lorem laboris aliqua aliquip.\r\n",
      imageUrl: "https://randomuser.me/api/portraits/women/22.jpg",
      price: 100,
      createdDate: new Date('3-3-2024'),
      releaseCount: 10,
      user: this.fakeUser
    },
    {
      id:2,
      title: 'A portrait',
      description: "Aliquip consequat culpa incididunt et veniam occaecat. Proident minim velit aliqua laborum ut labore do est. Est laborum est ea cupidatat sunt sunt nostrud sit aliqua reprehenderit.\r\n",
      imageUrl: "https://randomuser.me/api/portraits/women/50.jpg",
      price: 100,
      createdDate: new Date('3-3-2024'),
      releaseCount: 10,
      user: this.fakeUser
    },
    {
      id:3,
      title: 'A portrait',
      description: "Magna incididunt fugiat dolore id. Lorem cillum voluptate et dolor mollit et minim. Exercitation occaecat ipsum eu elit anim aliquip mollit est ullamco. Sit commodo non reprehenderit in nisi officia.\r\n",
      imageUrl: "https://randomuser.me/api/portraits/women/1.jpg",
      price: 100,
      createdDate: new Date('3-3-2024'),
      releaseCount: 10,
      user: this.fakeUser
    },
    {
      id:4,
      title: 'A portrait',
      description: "Pariatur officia aute nisi et cillum veniam nostrud mollit nulla ipsum. Incididunt esse proident proident pariatur id ex officia ea minim. Et id nulla ad qui excepteur aliquip aliqua.\r\n",
      imageUrl: "https://randomuser.me/api/portraits/women/8.jpg",
      price: 100,
      createdDate: new Date('3-3-2024'),
      releaseCount: 10,
      user: this.fakeUser
    },
    {
      id:5,
      title: 'A portrait',
      description: "Aliquip dolore commodo reprehenderit nisi. Labore irure minim aliqua commodo quis occaecat sunt excepteur tempor. Labore sint Lorem deserunt deserunt amet laboris aliqua sit culpa dolor occaecat deserunt. Eiusmod in ex pariatur laboris fugiat officia fugiat esse cupidatat fugiat laboris.\r\n",
      imageUrl: "https://randomuser.me/api/portraits/women/5.jpg",
      price: 100,
      createdDate: new Date('3-3-2024'),
      releaseCount: 10,
      user: this.fakeUser
    }
  ];

  constructor() { }

  getArtworks() {
    return this.artworks;
  }
}
