import React, { Component } from 'react';

export class ConfigMaker extends Component {
    static displayName = ConfigMaker.name;
    static dataToSend = [];

    constructor(props) {
        super(props);
        this.state = { apps: [], loading: true };
    }
    reset() {
        this.setState({ apps: [], loading: true });
    }

    componentDidMount() {
        this.populateSteamAppData();
    }

    async populateSteamAppData() {
        const response = await fetch('api/steamapp');
        const data = await response.json();
        this.setState({ apps: data, loading: false });
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
            : ConfigMaker.renderSteamAppsTable(this.state.apps);

        return (
            <div>
                <form>
                    <input
                        type="text"
                        name="price"
                        onChange={this.changeHandler}
                        placeholder="Price"
                        value={this.dataToSend}
                    />
                    <button>Add new item</button>
                </form>
                <br />
                <br />
                {contents}
            </div>
        );
    }
}
