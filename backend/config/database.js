const mongoose = require('mongoose');
const MONGO_URI = process.env.MONGO_URI;

const connectDb = async () => {
    try{
        mongoose.connect(MONGO_URI)
        .then(() => console.log('Database connected'))
        .catch(err => console.log('ERROR: ',err));
    } catch(err){
        console.log('Unable to connect to database.',err);
    }
}

module.exports = connectDb;