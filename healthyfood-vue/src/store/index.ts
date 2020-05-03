import { IDrinkEdit } from './../domain/IDrink/IDrinkEdit';
import { IDrinkCreate } from './../domain/IDrink/IDrinkCreate';
import { IPersonEdit } from './../domain/IPerson/IPersonEdit';
import { IChangePasswordDTO } from './../types/IChangePasswordDTO';
import { IChangeEmailDTO } from './../types/IChangeEmailDTO';
import { IRegisterDTO } from './../types/IRegisterDTO';
import { ITownEdit } from './../domain/ITown/ITownEdit';
import { ITownCreate } from './../domain/ITown/ITownCreate';
import Vue from 'vue'
import Vuex from 'vuex'
import { ILoginDTO } from '@/types/ILoginDTO';
import { AccountApi } from '@/services/AccountApi';

import { TownApi } from '@/services/TownApi';
import { ITown } from '../domain/ITown/ITown';
import { IPerson } from '@/domain/IPerson/IPerson';
import { PersonApi } from '@/services/PersonApi';
import { IPersonCreate } from '@/domain/IPerson/IPersonCreate';
import { IDrink } from '@/domain/IDrink/IDrink';
import { DrinkApi } from '@/services/DrinkApi';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        jwt: null as string | null,

        towns: [] as ITown[],
        town: null as ITown | null,

        persons: [] as IPerson[],
        person: null as IPerson | null,

        drinks: [] as IDrink[],
        drink: null as IDrink | null,

        userRole: null as string | null,
        userEmail: null as string | null,
        password: null as string | null
    },
    mutations: {
        setJwt(state, jwt: string | null) {
            state.jwt = jwt;
        },

        setTowns(state, towns: ITown[]) {
            state.towns = towns;
        },
        setTown(state, town: ITown) {
            state.town = town;
        },

        setPersons(state, persons: IPerson[]) {
            state.persons = persons;
        },
        setPerson(state, person: IPerson) {
            state.person = person;
        },

        setDrinks(state, drinks: IDrink[]) {
            state.drinks = drinks;
        },
        setDrink(state, drink: IDrink) {
            state.drink = drink;
        },

        setUserRole(state, userRole: string | null) {
            state.userRole = userRole;
        },
        setUserEmail(state, userEmail: string | null) {
            state.userEmail = userEmail;
        },
        setPassword(state, password: string | null) {
            state.password = password;
        }
    },
    getters: {
        isAuthenticated(context): boolean {
            return context.jwt !== null;
        },
        userRole(context): string | null {
            return context.userRole;
        },
        userEmail(context): string | null {
            return context.userEmail;
        },

        password(context): string | null {
            return context.password;
        },

        town(context): ITown {
            return context.town as ITown;
        },

        person(context): IPerson {
            return context.person as IPerson;
        },

        drink(context): IDrink {
            return context.drink as IDrink;
        }
    },
    actions: {
        clearJwt(context): void {
            context.commit('setJwt', null);
        },
        async changeEmail(context, emailDTO: IChangeEmailDTO): Promise<boolean> {
            const jwt = await AccountApi.changeEmail(emailDTO);
            context.commit('setJwt', jwt);
            return jwt !== null;
        },
        async changePassword(context, passwordDTO: IChangePasswordDTO): Promise<boolean> {
            const jwt = await AccountApi.changePassword(passwordDTO);
            context.commit('setJwt', jwt);
            return jwt !== null;
        },
        async registerUser(context, registerDTO: IRegisterDTO): Promise<boolean> {
            const jwt = await AccountApi.register(registerDTO);
            context.commit('setJwt', jwt);
            context.commit('setUserEmail', registerDTO.email)
            context.commit('setPassword', registerDTO.password)
            return jwt !== null;
        },
        async authenticateUser(context, loginDTO: ILoginDTO): Promise<boolean> {
            const jwt = await AccountApi.getJwt(loginDTO);
            context.commit('setJwt', jwt);
            context.commit('setUserEmail', loginDTO.email)
            context.commit('setPassword', loginDTO.password)
            return jwt !== null;
        },

        async getTowns(context): Promise<void> {
            const towns = await TownApi.getAllTowns();
            context.commit('setTowns', towns);
        },
        async getTown(context, id: string): Promise<boolean> {
            const town = await TownApi.getTown(id);
            context.commit('setTown', town);
            return true;
        },
        async deleteTown(context, id: string): Promise<void> {
            console.log('deleteTown', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await TownApi.deleteTown(id, context.state.jwt);
                await context.dispatch('getTowns');
            }
        },
        async createTown(context, town: ITownCreate): Promise<void> {
            console.log('createTown', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await TownApi.createTown(town, context.state.jwt);
                await context.dispatch('getTowns');
            }
        },
        async updateTown(context, town: ITownEdit): Promise<void> {
            console.log('editTown', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                context.commit('setTown', town)
                await TownApi.updateTown(town, context.state.jwt);
                await context.dispatch('getTowns');
            }
        },

        async getPersons(context): Promise<void> {
            const persons = await PersonApi.getAllPersons();
            context.commit('setPersons', persons);
        },
        async getPerson(context, id: string): Promise<boolean> {
            const person = await PersonApi.getPerson(id);
            context.commit('setPerson', person);
            return true;
        },
        async deletePerson(context, id: string): Promise<void> {
            console.log('deletePerson', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await PersonApi.deletePerson(id, context.state.jwt);
                await context.dispatch('getPersons');
            }
        },
        async createPerson(context, person: IPersonCreate): Promise<void> {
            console.log('createTown', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await PersonApi.createPerson(person, context.state.jwt);
                await context.dispatch('getPersons');
            }
        },
        async updatePerson(context, person: IPersonEdit): Promise<void> {
            console.log('editPerson', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                context.commit('setPerson', person)
                await PersonApi.updatePerson(person, context.state.jwt);
                await context.dispatch('getPersons');
            }
        },

        async getDrinks(context): Promise<void> {
            const drinks = await DrinkApi.getAllDrinks();
            context.commit('setDrinks', drinks);
        },
        async getDrink(context, id: string): Promise<boolean> {
            const drink = await DrinkApi.getDrink(id);
            context.commit('setDrink', drink);
            return true;
        },
        async deleteDrink(context, id: string): Promise<void> {
            console.log('deleteDrink', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await DrinkApi.deleteDrink(id, context.state.jwt);
                await context.dispatch('getDrinks');
            }
        },
        async createDrink(context, drink: IDrinkCreate): Promise<void> {
            console.log('createDrink', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await DrinkApi.createDrink(drink, context.state.jwt);
                await context.dispatch('getDrinks');
            }
        },
        async updateDrink(context, drink: IDrinkEdit): Promise<void> {
            console.log('editDrink', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                context.commit('setDrink', drink)
                await DrinkApi.updateDrink(drink, context.state.jwt);
                await context.dispatch('getDrinks');
            }
        }
    },
    modules: {
    }
})
