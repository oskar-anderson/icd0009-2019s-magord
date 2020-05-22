import { RestaurantService } from './service/restaurant-service';
import { IRestaurant } from './domain/IRestaurant/IRestaurant';
import { AppState } from './state/app-state';
import { autoinject, PLATFORM } from 'aurelia-framework';
import { RouterConfiguration, Router } from 'aurelia-router';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';

@autoinject
export class App {
    router?: Router;
    private _restaurants: IRestaurant[] = []
    private _alert: IAlertData | null = null;

    constructor(private appState: AppState, private restaurantService: RestaurantService) {

    }

    attached() {
        this.restaurantService.getRestaurants().then(
            response => {
                if (response.statusCode >= 200 && response.statusCode < 300) {
                    this._restaurants = response.data!;
                } else {
                    // show error message
                    this._alert = {
                        message: response.statusCode.toString() + ' - ' + response.errorMessage,
                        type: AlertType.Danger,
                        dismissable: true,
                    }
                }
            }
        );
    }


    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;

        config.title = "HealthyFood";

        config.map([
            { route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home' },

            { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login' },
            { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register' },

            { route: ['account/manage'], name: 'account-manage', moduleId: PLATFORM.moduleName('views/account/manage/index'), nav: false, title: 'Manage account' },
            { route: ['account/manageEmail'], name: 'account-manageEmail', moduleId: PLATFORM.moduleName('views/account/manage/email'), nav: false, title: 'Manage email' },
            { route: ['account/managePassword'], name: 'account-managePassword', moduleId: PLATFORM.moduleName('views/account/manage/password'), nav: false, title: 'Manage password' },
            { route: ['account/manageContacts'], name: 'account-manageContacts', moduleId: PLATFORM.moduleName('views/account/manage/contacts'), nav: false, title: 'Manage contacts' },



            { route: ['areas', 'areas/index'], name: 'areas-index', moduleId: PLATFORM.moduleName('views/areas/index'), nav: true, title: 'Areas' },
            { route: ['areas/details/:id?'], name: 'areas-details', moduleId: PLATFORM.moduleName('views/areas/details'), nav: false, title: 'Areas details' },
            { route: ['areas/edit/:id?'], name: 'areas-edit', moduleId: PLATFORM.moduleName('views/areas/edit'), nav: false, title: 'Areas Edit' },
            { route: ['areas/create'], name: 'areas-create', moduleId: PLATFORM.moduleName('views/areas/create'), nav: false, title: 'Areas Create' },


            { route: ['bills', 'bills/index'], name: 'bills-index', moduleId: PLATFORM.moduleName('views/bills/index'), nav: true, title: 'Bills' },
            { route: ['bills/details/:id'], name: 'bills-details', moduleId: PLATFORM.moduleName('views/bills/details'), nav: false, title: 'Bills details' },
            { route: ['bills/edit/:id'], name: 'bills-edit', moduleId: PLATFORM.moduleName('views/bills/edit'), nav: false, title: 'Bills Edit' },
            { route: ['bills/create'], name: 'bills-create', moduleId: PLATFORM.moduleName('views/bills/create'), nav: false, title: 'Bills Create' },


            { route: ['campaigns', 'campaigns/index'], name: 'campaigns-index', moduleId: PLATFORM.moduleName('views/campaigns/index'), nav: true, title: 'Campaigns' },
            { route: ['campaigns/details/:id?'], name: 'campaigns-details', moduleId: PLATFORM.moduleName('views/campaigns/details'), nav: false, title: 'Campaigns details' },
            { route: ['campaigns/edit/:id?'], name: 'campaigns-edit', moduleId: PLATFORM.moduleName('views/campaigns/edit'), nav: false, title: 'Campaigns Edit' },
            { route: ['campaigns/create'], name: 'campaigns-create', moduleId: PLATFORM.moduleName('views/campaigns/create'), nav: false, title: 'Campaigns Create' },


            { route: ['contacts/details/:id?'], name: 'contacts-details', moduleId: PLATFORM.moduleName('views/contacts/details'), nav: false, title: 'Contacts details' },
            { route: ['contacts/edit/:id?'], name: 'contacts-edit', moduleId: PLATFORM.moduleName('views/contacts/edit'), nav: false, title: 'Contacts Edit' },
            { route: ['contacts/create'], name: 'contacts-create', moduleId: PLATFORM.moduleName('views/contacts/create'), nav: false, title: 'Contacts Create' },



            { route: ['contacttypes', 'contacttypes/index'], name: 'contacttypes-index', moduleId: PLATFORM.moduleName('views/contacttypes/index'), nav: true, title: 'Contact types' },
            { route: ['contacttypes/details/:id?'], name: 'contacttypes-details', moduleId: PLATFORM.moduleName('views/contacttypes/details'), nav: false, title: 'Contact Types details' },
            { route: ['contacttypes/edit/:id?'], name: 'contacttypes-edit', moduleId: PLATFORM.moduleName('views/contacttypes/edit'), nav: false, title: 'Contact Types Edit' },
            { route: ['contacttypes/create'], name: 'contacttypes-create', moduleId: PLATFORM.moduleName('views/contacttypes/create'), nav: false, title: 'Contact Types Create' },


            { route: ['drinks', 'drinks/index'], name: 'drinks-index', moduleId: PLATFORM.moduleName('views/drinks/index'), nav: true, title: 'Drinks' },
            { route: ['drinks/details/:id?'], name: 'drinks-details', moduleId: PLATFORM.moduleName('views/drinks/details'), nav: false, title: 'Drinks details' },
            { route: ['drinks/edit/:id?'], name: 'drinks-edit', moduleId: PLATFORM.moduleName('views/drinks/edit'), nav: false, title: 'Drinks Edit' },
            { route: ['drinks/create'], name: 'drinks-create', moduleId: PLATFORM.moduleName('views/drinks/create'), nav: false, title: 'Drinks Create' },



            { route: ['foods', 'foods/index'], name: 'foods-index', moduleId: PLATFORM.moduleName('views/foods/index'), nav: true, title: 'Foods' },
            { route: ['foods/details/:id?'], name: 'foods-details', moduleId: PLATFORM.moduleName('views/foods/details'), nav: false, title: 'Foods details' },
            { route: ['foods/edit/:id?'], name: 'foods-edit', moduleId: PLATFORM.moduleName('views/foods/edit'), nav: false, title: 'Foods Edit' },
            { route: ['foods/create'], name: 'foods-create', moduleId: PLATFORM.moduleName('views/foods/create'), nav: false, title: 'Foods Create' },



            { route: ['foodtypes', 'foodtypes/index'], name: 'foodtypes-index', moduleId: PLATFORM.moduleName('views/foodtypes/index'), nav: true, title: 'Food types' },
            { route: ['foodtypes/details/:id?'], name: 'foodtypes-details', moduleId: PLATFORM.moduleName('views/foodtypes/details'), nav: false, title: 'Food Types details' },
            { route: ['foodtypes/edit/:id?'], name: 'foodtypes-edit', moduleId: PLATFORM.moduleName('views/foodtypes/edit'), nav: false, title: 'Food Types Edit' },
            { route: ['foodtypes/delete/:id?'], name: 'foodtypes-delete', moduleId: PLATFORM.moduleName('views/foodtypes/delete'), nav: false, title: 'Food Types Delete' },
            { route: ['foodtypes/create'], name: 'foodtypes-create', moduleId: PLATFORM.moduleName('views/foodtypes/create'), nav: false, title: 'Food Types Create' },


            { route: ['ingredients', 'ingredients/index'], name: 'ingredients-index', moduleId: PLATFORM.moduleName('views/ingredients/index'), nav: true, title: 'Ingredients' },
            { route: ['ingredients/details/:id?'], name: 'ingredients-details', moduleId: PLATFORM.moduleName('views/ingredients/details'), nav: false, title: 'Ingredients details' },
            { route: ['ingredients/edit/:id?'], name: 'ingredients-edit', moduleId: PLATFORM.moduleName('views/ingredients/edit'), nav: false, title: 'Ingredients Edit' },
            { route: ['ingredients/create'], name: 'ingredients-create', moduleId: PLATFORM.moduleName('views/ingredients/create'), nav: false, title: 'Ingredients Create' },


            { route: ['orders', 'orders/index'], name: 'orders-index', moduleId: PLATFORM.moduleName('views/orders/index'), nav: true, title: 'My Orders' },
            { route: ['orders/details/:id?'], name: 'orders-details', moduleId: PLATFORM.moduleName('views/orders/details'), nav: false, title: 'Orders details' },
            { route: ['orders/edit/:id?'], name: 'orders-edit', moduleId: PLATFORM.moduleName('views/orders/edit'), nav: false, title: 'Orders Edit' },
            { route: ['orders/create'], name: 'orders-create', moduleId: PLATFORM.moduleName('views/orders/create'), nav: false, title: 'Orders Create' },



            { route: ['orderitems', 'orderitems/index'], name: 'orderitems-index', moduleId: PLATFORM.moduleName('views/orderitems/index'), nav: true, title: 'My cart' },
            { route: ['orderitems/details/:id?'], name: 'orderitems-details', moduleId: PLATFORM.moduleName('views/orderitems/details'), nav: false, title: 'Order items details' },
            { route: ['orderitems/edit/:id?'], name: 'orderitems-edit', moduleId: PLATFORM.moduleName('views/orderitems/edit'), nav: false, title: 'Order items Edit' },
            { route: ['orderitems/create'], name: 'orderitems-create', moduleId: PLATFORM.moduleName('views/orderitems/create'), nav: false, title: 'Order items Create' },



            { route: ['ordertypes', 'ordertypes/index'], name: 'ordertypes-index', moduleId: PLATFORM.moduleName('views/ordertypes/index'), nav: true, title: 'Order types' },
            { route: ['ordertypes/details/:id?'], name: 'ordertypes-details', moduleId: PLATFORM.moduleName('views/ordertypes/details'), nav: false, title: 'Order Types details' },
            { route: ['ordertypes/edit/:id?'], name: 'ordertypes-edit', moduleId: PLATFORM.moduleName('views/ordertypes/edit'), nav: false, title: 'Order Types Edit' },
            { route: ['ordertypes/delete/:id?'], name: 'ordertypes-delete', moduleId: PLATFORM.moduleName('views/ordertypes/delete'), nav: false, title: 'Order Types Delete' },
            { route: ['ordertypes/create'], name: 'ordertypes-create', moduleId: PLATFORM.moduleName('views/ordertypes/create'), nav: false, title: 'Order Types Create' },



            { route: ['payments', 'payments/index'], name: 'payments-index', moduleId: PLATFORM.moduleName('views/payments/index'), nav: true, title: 'Payments' },
            { route: ['payments/details/:id?'], name: 'payments-details', moduleId: PLATFORM.moduleName('views/payments/details'), nav: false, title: 'Payments details' },
            { route: ['payments/edit/:id?'], name: 'payments-edit', moduleId: PLATFORM.moduleName('views/payments/edit'), nav: false, title: 'Payments Edit' },
            { route: ['payments/create'], name: 'payments-create', moduleId: PLATFORM.moduleName('views/payments/create'), nav: false, title: 'Payments Create' },


            { route: ['paymenttypes', 'paymenttypes/index'], name: 'paymenttypes-index', moduleId: PLATFORM.moduleName('views/paymenttypes/index'), nav: true, title: 'Payment types' },
            { route: ['paymenttypes/details/:id?'], name: 'paymenttypes-details', moduleId: PLATFORM.moduleName('views/paymenttypes/details'), nav: false, title: 'Payment Types details' },
            { route: ['paymenttypes/edit/:id?'], name: 'paymenttypes-edit', moduleId: PLATFORM.moduleName('views/paymenttypes/edit'), nav: false, title: 'Payment Types Edit' },
            { route: ['paymenttypes/create'], name: 'paymenttypes-create', moduleId: PLATFORM.moduleName('views/paymenttypes/create'), nav: false, title: 'Payment Types Create' },


            { route: ['prices', 'prices/index'], name: 'prices-index', moduleId: PLATFORM.moduleName('views/prices/index'), nav: true, title: 'Prices' },
            { route: ['prices/details/:id?'], name: 'prices-details', moduleId: PLATFORM.moduleName('views/prices/details'), nav: false, title: 'Prices details' },
            { route: ['prices/edit/:id?'], name: 'prices-edit', moduleId: PLATFORM.moduleName('views/prices/edit'), nav: false, title: 'Prices Edit' },
            { route: ['prices/create'], name: 'prices-create', moduleId: PLATFORM.moduleName('views/prices/create'), nav: false, title: 'Prices Create' },


            { route: ['restaurants', 'restaurants/index'], name: 'restaurants-index', moduleId: PLATFORM.moduleName('views/restaurants/index'), nav: true, title: 'Restaurants' },
            { route: ['restaurants/details/:id?'], name: 'restaurants-details', moduleId: PLATFORM.moduleName('views/restaurants/details'), nav: false, title: 'Restaurants details' },
            { route: ['restaurants/edit/:id?'], name: 'restaurants-edit', moduleId: PLATFORM.moduleName('views/restaurants/edit'), nav: false, title: 'Restaurants Edit' },
            { route: ['restaurants/create'], name: 'restaurants-create', moduleId: PLATFORM.moduleName('views/restaurants/create'), nav: false, title: 'Restaurants Create' },


            { route: ['towns', 'towns/index'], name: 'towns-index', moduleId: PLATFORM.moduleName('views/towns/index'), nav: true, title: 'Towns' },
            { route: ['towns/details/:id?'], name: 'towns-details', moduleId: PLATFORM.moduleName('views/towns/details'), nav: false, title: 'Towns Details' },
            { route: ['towns/edit/:id?'], name: 'towns-edit', moduleId: PLATFORM.moduleName('views/towns/edit'), nav: false, title: 'Towns Edit' },
            { route: ['towns/delete/:id?'], name: 'towns-delete', moduleId: PLATFORM.moduleName('views/towns/delete'), nav: false, title: 'Towns Delete' },
            { route: ['towns/create'], name: 'towns-create', moduleId: PLATFORM.moduleName('views/towns/create'), nav: false, title: 'Towns Create' }
        ]
        );

        if (this.appState.jwt !== null) {
            config.mapUnknownRoutes('views/home/index');
        } else {
            config.mapUnknownRoutes('views/account/login')
        }
    }

    logoutOnClick() {
        this.appState.jwt = null;
        this.router!.navigateToRoute('account-login');
    }

}
