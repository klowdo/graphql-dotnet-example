import { Component, OnInit } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from 'graphql-tag';


// We use the gql tag to parse our query string into a query document
const CurrentUserForProfile = gql`
  fragment engagementFragment on Engagement {
    name
    
  }
  query CurrentUserForProfile {
    customers{
      name
      notes
      birthDate
      engagements{
        ...engagementFragment
      }
    }
  }
`;
const submitCustomer = gql`
  mutation submitCustomer($name:String!) {
    addCustomer(input:{
      clientMutationId:"corrId",
      newCustomer:{
        newName: $name
        birthDate: "2016-01-01"
      }
    }) {
      clientMutationId
      customer{
        name
        birthDate
      }
    }
}
`;
interface QueryResponse {
    customers: any;
    loading: boolean;
}
interface MutationResponse {
  customer: any;
  loading: boolean;
}

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit  {
    loading: boolean;
    customers: any;
    newCustomerName:string;
    newCustomerDate:string;
    constructor(private apollo: Apollo) {}
    
    createCustomer(){
      this.apollo.mutate<MutationResponse>({
        mutation: submitCustomer,
        variables: {
          name: this.newCustomerName,
        }
      }).subscribe(({ data }) => {
        console.log('got data', data);
        if(data != null){
          var customer = data.customer;
          this.customers = [...this.customers, ]
        }
       
      },(error) => {
        console.log('there was an error sending the query', error);
      });
    }

    ngOnInit() {
        this.loading = true;
        this.apollo.watchQuery<QueryResponse>({
          query: CurrentUserForProfile,
           pollInterval: 200
        }).subscribe(({data: {loading , customers }}) => {
          
          this.loading = loading;
          this.customers = customers;
  
      
        });
      }
}
