// File: src/app/_models/artwork-admin-dto.ts

export interface ArtworkAdminDTO {
    id: number;
    title: string;
    description?: string;
    price: number;
    releaseCount: number;
    owner: string;
    imageUrl: string;
    createdDate: Date;
    status: number;
    // Bạn có thể bỏ comment cho hai dòng dưới nếu bạn muốn sử dụng các đối tượng liên quan như artwork images hoặc user info.
    // artworkImages: ArtworkImageDTO[];
    // user: ArtworkUserDTO;
  }
  