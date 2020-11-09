import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { steamapps: [], loading: true };
    }

    componentDidMount() {
        this.populateSteamAppData();
    }

    static renderSteamAppsTable(steamapps) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>App Name</th>
                        <th>App ID</th>
                    </tr>
                </thead>
                <tbody>
                    {steamapps.map(steamapp =>
                        <tr key={steamapp.appid}>
                            <td>{steamapp.name}</td>
                            <td>{steamapp.appid}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderSteamAppsTable(this.state.steamapps);

        return (
            <div>
                <h1 id="tabelLabel" >Steam Apps Owned</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateSteamAppData() {
        const response = await fetch('api/steamapp');
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
}
