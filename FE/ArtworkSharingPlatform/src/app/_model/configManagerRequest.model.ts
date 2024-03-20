  export interface ConfigManagerRequest {
      id: number;
      configDate: Date;
      isServicePackageConfig: boolean;
      isPhysicalImageConfig: boolean;
      isGeneralConfig: boolean;
      logoUrl: string;
      myPhoneNumber: string;
      address: string;
      isPagingConfig: boolean;
      totalItemPerPage: number;
      rowSize: number;
      isAdvertisementConfig: boolean;
      companyName: string;
      companyPhoneNumber: string;
      companyEmail: string;
    }