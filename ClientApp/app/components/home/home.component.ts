import { Component, OnInit } from '@angular/core';

import { Apollo } from 'apollo-angular';
import gql from 'graphql-tag';

// We use the gql tag to parse our query string into a query document
const CurrentUserForProfile = gql`
  query CurrentUserForProfile {
    customers{
      name,
      birthDate
    }
  }
`;

interface QueryResponse {
    customers: boolean;
    loading: any;
}


@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent  implements OnInit  {
    loading: boolean;
    customers: any;
    constructor(private apollo: Apollo) {}
    

    ngOnInit() {
        this.apollo.watchQuery<QueryResponse>({
          query: CurrentUserForProfile
        }).subscribe(({data: {loading , customers }}) => {
          this.loading = loading;
          this.customers = customers;
        });
      }
}
