const mongoose = require('mongoose');
const validator = require('validator');
const bcrypt = require('bcryptjs');

const userSchema = new mongoose.Schema({
    username:{
        type: String,
        required: [true, 'A username is required'],
        maxlength: [16, 'The limit is 16 characters'],
    },
    email: {
        type: String,
        required: [true, 'An email is required'],
        unique: [true, 'This email is already in use'],
        validate: [validator.isEmail, 'Invalid email'],
    },
    password: {
        type: String,
        required: [true, 'A password is required'],
        minlength: [8, 'The password needs to have at least 8 digits'],
        maxlength: [16, 'The limit is 16 characters'],
    }
}, {timestamps: true});

userSchema.pre('save', async function(next){

    if(!this.isModified('password')){
        return next();
    }

    try{

        const normalizedPassword = this.password.normalize('NFC');

        const salt = await bcrypt.genSalt(10);

        this.password = await bcrypt.hash(normalizedPassword, salt);

        next();
    } catch(err){
        next(err);
    }
});

userSchema.methods.comparePassword = async function(typedPassword) {
    try {

        const typedNormalized = typedPassword.normalize('NFC');

        return await bcrypt.compare(typedNormalized, this.password);
    } catch(err){
        throw new Error(err);
    }
};

module.exports = mongoose.model('User', userSchema);