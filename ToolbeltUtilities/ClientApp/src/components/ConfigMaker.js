import React, { Component } from 'react';

export class ConfigMaker extends Component {
    static displayName = ConfigMaker.name;

    constructor(props) {
        super(props);
        this.state = { apps: [], loading: true };
    }

    componentDidMount() {
        this.populateSteamAppData();
    }

    async populateSteamAppData() {
        const response = await fetch('api/steamapp');
        const data = await response.json();
        this.setState({ apps: data, loading: false });
    }

    render() {
        return (
            <div>
                <form>
                    <input
                        type="text"
                        name="price"
                        onChange={this.changeHandler}
                        placeholder="Price"
                        value={this.state.item.price}
                    />

                    <input
                        type="text"
                        name="description"
                        onChange={this.changeHandler}
                        placeholder="Description"
                        value={this.state.item.description}
                    />

                    <button>Add new item</button>
                </form>
            </div>
        );
    }
}
